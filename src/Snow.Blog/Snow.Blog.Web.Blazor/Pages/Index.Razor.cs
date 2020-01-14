using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace Snow.Blog.Web.Pages
{
    public partial class Index
    {
        [Parameter]
        public int? Page { get; set; }

        [Parameter]
        public int? CategoryId { get; set; }

        protected override void OnInitialized()
        {
            // TODO:项目运行请求“/”2次
            Page = Page.HasValue ? Page : 1;
        }

        private Canvas2DContext _context;

        protected BECanvasComponent _canvasReference;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            this._context = await this._canvasReference.CreateCanvas2DAsync();

            Timer timer = new Timer(delegate
             {
                 NoteStoppedTyping();
             }, null, 0, 1000);
        }

        private void NoteStoppedTyping()
        {
            Task.Run(async () =>
            {
                await DrawClockAsync();
            });
        }

        private string Search;

        #region 画时钟

        #region 核心方法

        /// <summary>
        /// 画时钟
        /// </summary>
        /// <returns></returns>
        private async Task DrawClockAsync()
        {
            long width = _canvasReference.Width
                , height = _canvasReference.Height;
            // 半径
            double radius = width / 2;
            DateTime now = DateTime.Now;
            await ClearAsync(radius);
            // TODO:闪屏与卡死
            await DrawCicleAsync("red", radius, "#ebf0eb");
            //await DrawHourLineAsync(radius);
            //await DrawMinuteLineAsync(radius);
            //await DrawHourNumberAsync(radius);
            //await DrawHourHandAsync(now.Hour, radius);
            //await DrawMinuteHandAsync(now.Minute, radius);
            //await DrawSecondHandAsync(now.Second, radius);

            Task hourLine = DrawHourLineAsync(radius);
            Task minuteLine = DrawMinuteLineAsync(radius);
            Task hourNumber = DrawHourNumberAsync(radius);
            Task hourHand = DrawHourHandAsync(now.Hour, radius);
            Task minuteHand = DrawMinuteHandAsync(now.Minute, radius);
            Task secondHand = DrawSecondHandAsync(now.Second, radius);
            await Task.WhenAll(hourLine, minuteLine, hourNumber, hourHand, minuteHand, secondHand);
        }

        /// <summary>
        /// 画表盘圆
        ///</summary>
        ///<param name="strokeStyle"></param>
        ///<param name="radius">半径</param>
        ///<param name="fillStyle">填充色</param>
        /// <returns></returns>
        private async Task DrawCicleAsync(string strokeStyle, double radius, string fillStyle)
        {
            await _context.SaveAsync();
            await _context.TranslateAsync(radius, radius);
            await _context.BeginPathAsync();
            await _context.SetStrokeStyleAsync(strokeStyle);
            await _context.ArcAsync(0, 0, radius, 0, 2 * Math.PI, true);
            await _context.SetFillStyleAsync(fillStyle);
            await _context.FillAsync();
            await _context.RestoreAsync();
        }

        /// <summary>
        /// 画小时刻度
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        private async Task DrawHourLineAsync(double radius)
        {
            await DrawLineByCicleAsync(12, 8, 2, "#0094ff", radius);
        }

        /// <summary>
        /// 画分钟刻度
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        private async Task DrawMinuteLineAsync(double radius)
        {
            await DrawLineByCicleAsync(60, 5, 1, "red", radius, i => i % 5 == 0);
        }

        /// <summary>
        /// 画小时数字
        /// </summary>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawHourNumberAsync(double radius)
        {
            await _context.SaveAsync();
            //await _context.TranslateAsync(radius, radius);
            await _context.BeginPathAsync();
            await _context.SetFontAsync("18px Arial");
            await _context.SetFillStyleAsync("#000");
            await _context.SetTextAlignAsync(TextAlign.Center);
            await _context.SetTextBaselineAsync(TextBaseline.Middle);
            List<Task> tasks = new List<Task>();
            for (int i = 1; i <= 12; i++)
            {
                tasks.Add(DrawOneHourNumberAsync(i, radius));
            }
            await Task.WhenAll(tasks);
            await _context.RestoreAsync();
        }

        /// <summary>
        /// 画时针
        /// </summary>
        /// <param name="hour">小时</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawHourHandAsync(int hour, double radius)
        {
            await _context.SaveAsync();
            await _context.TranslateAsync(radius, radius);
            double theta = (hour - 3) * 2 * Math.PI / 12;
            await _context.RotateAsync((float)theta);
            await _context.BeginPathAsync();
            await _context.MoveToAsync(-15, -5);
            await _context.LineToAsync(-15, 5);
            await _context.LineToAsync(radius * 0.6, 1);
            await _context.LineToAsync(radius * 0.6, -1);
            await _context.FillAsync();
            await _context.RestoreAsync();
        }

        /// <summary>
        /// 画分针
        /// </summary>
        /// <param name="minute">分钟</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawMinuteHandAsync(int minute, double radius)
        {
            await _context.SaveAsync();
            await _context.TranslateAsync(radius, radius);

            double theta = (minute - 15) * 2 * Math.PI / 60;
            // 旋转角度
            await _context.RotateAsync((float)theta);
            // 开始绘制
            await _context.BeginPathAsync();
            await _context.MoveToAsync(-15, -4); // 移动画笔到指定的坐标点(x, y)
            await _context.LineToAsync(-15, 4); // 使用直线连接当前端点和指定的坐标点(x,y)
            await _context.LineToAsync(radius * 0.75, 1);
            await _context.LineToAsync(radius * 0.75, -1);
            // 插件暂不支持渐变色
            await _context.FillAsync();
            await _context.RestoreAsync();

            //ctx.save();
            //var theta = (minute - 15) * 2 * Math.PI / 60;
            ////旋转角度
            //ctx.rotate(theta);
            ////开始绘制
            //ctx.beginPath();
            ////moveTo(x,y)：移动画笔到指定的坐标点(x,y)
            //ctx.moveTo(-15, -4);
            ////lineTo(x,y)：使用直线连接当前端点和指定的坐标点(x,y)
            //ctx.lineTo(-15, 4);
            //ctx.lineTo(clockRadius * 0.75, 1);
            //ctx.lineTo(clockRadius * 0.75, -1);
            //var canvasGradient = ctx.createLinearGradient(-15, -4, 0.6 * clockRadius, -15, 4, 0.6 * clockRadius);
            //canvasGradient.addColorStop(0.5, 'black');
            //canvasGradient.addColorStop(1, 'red');
            //ctx.fillStyle = canvasGradient;
            //ctx.fill();
            //ctx.restore();
        }

        /// <summary>
        /// 画秒针
        /// </summary>
        /// <param name="second">秒钟</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawSecondHandAsync(int second, double radius)
        {
            await _context.SaveAsync();
            await _context.TranslateAsync(radius, radius);
            double theta = (second - 15) * 2 * Math.PI / 60;
            // 旋转角度
            await _context.RotateAsync((float)theta);
            // 开始绘制
            await _context.BeginPathAsync();
            await _context.MoveToAsync(-15, -3); // 移动画笔到指定的坐标点(x, y)
            await _context.LineToAsync(-15, 3); // 使用直线连接当前端点和指定的坐标点(x,y)
            await _context.LineToAsync(radius * 0.85, 1);
            await _context.LineToAsync(radius * 0.85, -1);
            await _context.SetFillStyleAsync("#0f0");
            await _context.FillAsync();
            await _context.RestoreAsync();
        }

        #endregion 核心方法

        #region 辅助方法

        private async Task ClearAsync(double radius)
        {
            //await _context.MoveToAsync(0, 0);
            await _context.ClearRectAsync(-radius, -radius, radius * 2, radius * 2);
        }

        /// <summary>
        /// 画一个小时数字
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawOneHourNumberAsync(int index, double radius)
        {
            var theta = (index - 3) * (Math.PI * 2) / 12;
            var x = radius + radius * 0.8 * Math.Cos(theta);
            var y = radius + radius * 0.8 * Math.Sin(theta);
            await _context.FillTextAsync(index.ToString(), x, y);
        }

        /// <summary>
        /// 画圆内线
        /// </summary>
        /// <param name="count">个数</param>
        /// <param name="length">长度</param>
        /// <param name="width">宽度</param>
        /// <param name="color">颜色</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawLineByCicleAsync(int count, int length, int width, string color, double radius)
        {
            await DrawLineByCicleAsync(count, length, width, color, radius, null);
        }

        /// <summary>
        /// 画圆内线
        /// </summary>
        /// <param name="count">个数</param>
        /// <param name="length">长度</param>
        /// <param name="width">宽度</param>
        /// <param name="color">颜色</param>
        /// <param name="radius">半径</param>
        /// <param name="ignore">忽略方式</param>
        /// <returns></returns>
        private async Task DrawLineByCicleAsync(int count, int length, int width, string color, double radius, Func<int, bool> ignore)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < count; i++)
            {
                if (ignore != null && ignore(i))
                {
                    continue;
                }
                tasks.Add(DrawOneLineByCicleAsync(count, i, length, width, color, radius));
            }
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// 画一条圆内线
        /// </summary>
        /// <param name="count">个数</param>
        /// <param name="index">索引</param>
        /// <param name="length">长度</param>
        /// <param name="width">宽度</param>
        /// <param name="color">颜色</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        private async Task DrawOneLineByCicleAsync(int count, int index, int length, int width, string color, double radius)
        {
            int interval = 360 / count;
            await _context.SaveAsync();
            var angle = index * interval * Math.PI / 180;
            await _context.TranslateAsync(radius, radius);
            await _context.BeginPathAsync();
            await _context.SetStrokeStyleAsync(color);
            await _context.SetLineWidthAsync(width);
            await _context.MoveToAsync(radius * Math.Cos(angle), radius * Math.Sin(angle));
            await _context.LineToAsync((radius - length) * Math.Cos(angle), (radius - length) * Math.Sin(angle));
            await _context.StrokeAsync();
            await _context.RestoreAsync();
        }

        #endregion 辅助方法

        #endregion 画时钟
    }
}