/************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2010-2012 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   This program can be provided to you by Xceed Software Inc. under a
   proprietary commercial license agreement for use in non-Open Source
   projects. The commercial version of Extended WPF Toolkit also includes
   priority technical support, commercial updates, and many additional 
   useful WPF controls if you license Xceed Business Suite for WPF.

   Visit http://xceed.com and follow @datagrid on Twitter.

  **********************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

using Xceed.Wpf.Toolkit.Primitives;

namespace Xceed.Wpf.Toolkit
{
  public class TimeSpanUpDown : UpDownBase<TimeSpan?>
  {
    #region Members

    private List<DateTimeInfo> _dateTimeInfoList = new List<DateTimeInfo>();
    private DateTimeInfo _selectedDateTimeInfo;
    private bool _fireSelectionChangedEvent = true;
    private bool _processTextChanged = true;

    #endregion //Members

    #region Properties

    #region DefaultValue

    public static readonly DependencyProperty DefaultValueProperty = DependencyProperty.Register("DefaultValue", typeof(TimeSpan), typeof(TimeSpanUpDown), new UIPropertyMetadata(new TimeSpan()));
    public TimeSpan DefaultValue
    {
      get
      {
        return ( TimeSpan )this.GetValue( DefaultValueProperty );
      }
      set
      {
        this.SetValue( DefaultValueProperty, value );
      }
    }

    #endregion //DefaultValue

    #region Maximum



    #endregion //Maximum

    #region Minimum



    #endregion //Minimum

    #endregion //Properties

    #region Constructors

    static TimeSpanUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeSpanUpDown), new FrameworkPropertyMetadata(typeof(TimeSpanUpDown)));
    }

    public TimeSpanUpDown()
    {
      this.InitializeDateTimeInfoList();
    }

    #endregion //Constructors

    #region Base Class Overrides

    public override void OnApplyTemplate()
    {
      if( this.TextBox != null )
        this.TextBox.SelectionChanged -= this.TextBox_SelectionChanged;

      base.OnApplyTemplate();

      if( this.TextBox != null )
        this.TextBox.SelectionChanged += this.TextBox_SelectionChanged;
    }

    protected override void OnCultureInfoChanged( CultureInfo oldValue, CultureInfo newValue )
    {
      this.InitializeDateTimeInfoList();
    }

    protected override void OnIncrement()
    {
      if( this.Value.HasValue )
          this.UpdateTimeSpan(1);
      else
        this.Value = this.DefaultValue;
    }

    protected override void OnDecrement()
    {
      if( this.Value.HasValue )
          this.UpdateTimeSpan(-1);
      else
        this.Value = this.DefaultValue;
    }

    protected override void OnPreviewKeyDown( KeyEventArgs e )
    {
      switch( e.Key )
      {
        case Key.Enter:
          {
            if( !this.IsReadOnly )
            {
              this._fireSelectionChangedEvent = false;
              BindingExpression binding = BindingOperations.GetBindingExpression( this.TextBox, System.Windows.Controls.TextBox.TextProperty );
              binding.UpdateSource();
              this._fireSelectionChangedEvent = true;
              e.Handled = true;
            }
            return;
          }
        default:
          {
            this._fireSelectionChangedEvent = false;
            break;
          }
      }

      base.OnPreviewKeyDown( e );
    }

    protected override void OnTextChanged( string previousValue, string currentValue )
    {
      if( !this._processTextChanged )
        return;

      if( String.IsNullOrEmpty( currentValue ) )
      {
        this.Value = null;
        return;
      }

        TimeSpan current = this.Value.HasValue ? this.Value.Value : new TimeSpan();
      TimeSpan result;
      var success = TimeSpan.TryParse( currentValue, out result );
      currentValue = result.ToString();

      this.SyncTextAndValueProperties(true, currentValue);
    }

    protected override TimeSpan? ConvertTextToValue(string text)
    {
      if( string.IsNullOrEmpty( text ) )
        return null;

      return TimeSpan.Parse(text, this.CultureInfo);
    }

    protected override string ConvertValueToText()
    {
      if( this.Value == null )
        return string.Empty;

      return this.Value.Value.ToString();
    }

    protected override void SetValidSpinDirection()
    {
      //TODO: implement Minimum and Maximum
    }

    protected override void OnValueChanged(TimeSpan? oldValue, TimeSpan? newValue)
    {
      //whenever the value changes we need to parse out the value into out DateTimeInfo segments so we can keep track of the individual pieces
      //but only if it is not null
      if( newValue != null )
          this.ParseValueIntoTimeSpanInfo();

      base.OnValueChanged( oldValue, newValue );
    }

    #endregion //Base Class Overrides

    #region Event Hanlders

    void TextBox_SelectionChanged( object sender, RoutedEventArgs e )
    {
      if( this._fireSelectionChangedEvent )
        this.PerformMouseSelection();
      else
        this._fireSelectionChangedEvent = true;
    }

    #endregion //Event Hanlders

    #region Methods

    private void InitializeDateTimeInfoList()
    {
        this._dateTimeInfoList.Clear();

        if (this.Value.HasValue && this.Value.Value.TotalMilliseconds < 0)
            this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Other, Length = 1, Content = "-", IsReadOnly = true });
        if (this.Value.HasValue && this.Value.Value.Days != 0)
        {
            this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Day, Length = this.Value.Value.Days.ToString().Length, Format = "dd" });
            this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Other, Length = 1, Content = ".", IsReadOnly = true });
        }
        this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Hour24, Length = 2, Format = "hh" });
        this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Other, Length = 1, Content = ":", IsReadOnly = true });
        this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Minute, Length = 2, Format = "mm" });
        this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Other, Length = 1, Content = ":", IsReadOnly = true });
        this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Second, Length = 2, Format = "ss" });
        if (this.Value.HasValue && this.Value.Value.Milliseconds != 0)
        {
            this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Other, Length = 1, Content = ".", IsReadOnly = true });
            this._dateTimeInfoList.Add(new DateTimeInfo() { Type = DateTimePart.Second, Length = 7, Format = "fffffff" });
        }

        if (this.Value.HasValue)
            this.ParseValueIntoTimeSpanInfo();
    }

    private void ParseValueIntoTimeSpanInfo()
    {
      string text = string.Empty;

      this._dateTimeInfoList.ForEach( info =>
      {
        if( info.Format == null )
        {
          info.StartPosition = text.Length;
          info.Length = info.Content.Length;
          text += info.Content;
        }
        else
        {
          TimeSpan span = TimeSpan.Parse(this.Value.ToString());
          info.StartPosition = text.Length;
          info.Content = span.ToString(info.Format, this.CultureInfo.DateTimeFormat);
          if (info.Format == "dd")
            info.Content = Convert.ToInt32(info.Content).ToString();
          info.Length = info.Content.Length;
          text += info.Content;
        }
      } );
    }

    private void PerformMouseSelection()
    {
      this.InitializeDateTimeInfoList();

      this._dateTimeInfoList.ForEach( info =>
      {
        if( ( info.StartPosition <= this.TextBox.SelectionStart ) && ( this.TextBox.SelectionStart < ( info.StartPosition + info.Length ) ) )
        {
          this.Select( info );
          return;
        }
      } );
    }

    private void Select( DateTimeInfo info )
    {
      this._fireSelectionChangedEvent = false;
      this.TextBox.Select( info.StartPosition, info.Length );
      this._fireSelectionChangedEvent = true;
      this._selectedDateTimeInfo = info;
    }

    private void UpdateTimeSpan( int value )
    {
      this._fireSelectionChangedEvent = false;
      DateTimeInfo info = this._selectedDateTimeInfo;

      //this only occurs when the user manually type in a value for the Value Property
      if( info == null )
        info = this._dateTimeInfoList[ 0 ];

      try
      {
          switch (info.Type)
          {
              case DateTimePart.Day:
                  {
                      this.Value = ((TimeSpan)this.Value).Add(new TimeSpan(value, 0, 0, 0, 0));
                      break;
                  }
              case DateTimePart.Hour24:
                  {
                      this.Value = ((TimeSpan)this.Value).Add(new TimeSpan(0, value, 0, 0, 0));
                      break;
                  }
              case DateTimePart.Minute:
                  {
                      this.Value = this.Value = ((TimeSpan)this.Value).Add(new TimeSpan(0, 0, value, 0, 0));
                      break;
                  }
              case DateTimePart.Second:
                  {
                      this.Value = ((TimeSpan)this.Value).Add(new TimeSpan(0, 0, 0, value, 0));
                      break;
                  }
              case DateTimePart.Millisecond:
                  {
                      this.Value = ((TimeSpan)this.Value).Add(new TimeSpan(0, 0, 0, 0, value));
                      break;
                  }
              default:
                  {
                      break;
                  }
          }
      }
      catch
      {
        //this can occur if the date/time = 1/1/0001 12:00:00 AM which is the smallest date allowed.
        //I could write code that would validate the date each and everytime but I think that it would be more
        //efficient if I just handle the edge case and allow an exeption to occur and swallow it instead.
      }

      //InitializeDateTimeInfoList();

      //we loose our selection when the Value is set so we need to reselect it without firing the selection changed event
      this.TextBox.Select( info.StartPosition, info.Length );
      this._fireSelectionChangedEvent = true;
    }

    private void UpdateTextFormatting()
    {
      this._processTextChanged = false;

      if( this.Value.HasValue )
        this.Text = this.ConvertValueToText();

      this._processTextChanged = true;
    }

    #endregion //Methods
  }
}
