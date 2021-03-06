﻿using System;
using System.Graphics;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class DatePicker : GraphicsVisualView
    {
        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(DatePicker), default(DateTime), BindingMode.TwoWay,
                coerceValue: CoerceDate,
                propertyChanged: DatePropertyChanged,
                defaultValueCreator: (bindable) => DateTime.Today);

        public static readonly BindableProperty MinimumDateProperty =
            BindableProperty.Create(nameof(MinimumDate), typeof(DateTime), typeof(DatePicker), new DateTime(1900, 1, 1),
                validateValue: ValidateMinimumDate, coerceValue: CoerceMinimumDate);

        public static readonly BindableProperty MaximumDateProperty =
            BindableProperty.Create(nameof(MaximumDate), typeof(DateTime), typeof(DatePicker), new DateTime(2100, 12, 31),
                validateValue: ValidateMaximumDate, coerceValue: CoerceMaximumDate);

        static object CoerceDate(BindableObject bindable, object value)
        {
            var picker = (DatePicker)bindable;
            DateTime dateValue = ((DateTime)value).Date;

            if (dateValue > picker.MaximumDate)
                dateValue = picker.MaximumDate;

            if (dateValue < picker.MinimumDate)
                dateValue = picker.MinimumDate;

            return dateValue;
        }

        static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var datePicker = (DatePicker)bindable;
            datePicker.DateSelected?.Invoke(datePicker, new DateChangedEventArgs((DateTime)oldValue, (DateTime)newValue));
        }

        static bool ValidateMinimumDate(BindableObject bindable, object value)
        {
            return ((DateTime)value).Date <= ((DatePicker)bindable).MaximumDate.Date;
        }

        static object CoerceMinimumDate(BindableObject bindable, object value)
        {
            DateTime dateValue = ((DateTime)value).Date;
            var picker = (DatePicker)bindable;
            if (picker.Date < dateValue)
                picker.Date = dateValue;

            return dateValue;
        }

        static bool ValidateMaximumDate(BindableObject bindable, object value)
        {
            return ((DateTime)value).Date >= ((DatePicker)bindable).MinimumDate.Date;
        }

        static object CoerceMaximumDate(BindableObject bindable, object value)
        {
            DateTime dateValue = ((DateTime)value).Date;
            var picker = (DatePicker)bindable;
            if (picker.Date > dateValue)
                picker.Date = dateValue;

            return dateValue;
        }

        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public DateTime MaximumDate
        {
            get { return (DateTime)GetValue(MaximumDateProperty); }
            set { SetValue(MaximumDateProperty, value); }
        }

        public DateTime MinimumDate
        {
            get { return (DateTime)GetValue(MinimumDateProperty); }
            set { SetValue(MinimumDateProperty, value); }
        }

        public event EventHandler<DateChangedEventArgs> DateSelected;

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    HeightRequest = 56;
                    break;
                case VisualType.Cupertino:
                    HeightRequest = 36;
                    break;
                case VisualType.Fluent:
                    HeightRequest = 32;
                    break;
            }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawDatePickerBackground(canvas, dirtyRect);
            DrawDatePickerBorder(canvas, dirtyRect);
            DrawDatePickerIcon(canvas, dirtyRect);
            DrawDatePickerPlaceholder(canvas, dirtyRect);
            DrawDatePickerDate(canvas, dirtyRect);
        }

        protected virtual void DrawDatePickerBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialDatePickerBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoDatePickerBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentDatePickerBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawDatePickerBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialDatePickerBorder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoDatePickerBorder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentDatePickerBorder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawDatePickerIcon(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Fluent:
                    DrawFluentDatePickerIcon(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawDatePickerPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                    DrawMaterialDatePickerPlaceholder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawDatePickerDate(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialDatePickerDate(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoDatePickerDate(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentDatePickerDate(canvas, dirtyRect);
                    break;
            }
        }
    }
}