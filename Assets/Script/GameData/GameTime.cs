using System;
using UnityEngine;

public partial class MyGame
{
    [Serializable]
    public class GameTime : HuangDAPI.GMDate
	{
		public GameTime()
		{
			_year = 1;
			_month = 1;
			_day = 1;
		}

        public GameTime(GameTime time)
        {
            this._year = time.year;
            this._month = time.month;
            this._day = time.day;
        }

        public void Increase()
		{
			if (_day == 30)
			{
				if (_month == 12)
				{
					_year++;
					_month = 1;
                    if (incMonthEvent != null)
                    {
                        incMonthEvent();
                    }
                    if (incYearEvent != null)
                    {
                        incYearEvent();
                    }
                }
				else
				{
					_month++;
                    if (incMonthEvent != null)
                    {
                        incMonthEvent();
                    }
				}
				_day = 1;
			}
			else
			{
				_day++;
			}

            if(incDayEvent != null)
            {
                incDayEvent();
            }
            
        }

		public int year
		{
			get
			{
				return _year;
			}
		}

		public int month
		{
			get
			{
				return _month;
			}
		}

		public int day
		{
			get
			{
				return _day;
			}
		}

		public override string ToString()
		{
			return _year.ToString() + "年" + _month + "月" + _day + "日";
		}

		public bool Is(string str)
		{
			string[] arr = str.Split('/');
			if (arr.Length < 3)
			{
				throw new Exception();
			}

			if (arr[0] != "*")
			{
				if (Convert.ToInt16(arr[0]) != _year)
				{
					return false;
				}
			}

			if (arr[1] != "*")
			{
				if (Convert.ToInt16(arr[1]) != _month)
				{
					return false;
				}
			}

			if (arr[2] != "*")
			{
				if (Convert.ToInt16(arr[2]) != _day)
				{
					return false;
				}
			}

			return true;
		}

        public static int operator - (GameTime c1, GameTime c2)
        {
            int result = 0;
            if(c1.day < c2.day)
            {
                result += c1.day + 30 - c2.day;
                c1._month--;
            }
            else
            {
                result += c1.day - c2.day;
            }

            if(c1.month < c2.month)
            {
                result += (c1.month + 12 - c2.month) * 30;
                c1._year--;
            }
            else
            {
                result += (c1.month - c2.month) * 30;
            }

            if(c1.year < c2.year)
            {
                throw new ArgumentException(c1 + "is earlier than" + c2);
            }

            result += (c1.year - c2.year) * 12 * 30;

            return result;
        }

        public event Action incDayEvent;
        public event Action incMonthEvent;
        public event Action incYearEvent;

        [SerializeField]
		private int _year;
		[SerializeField]
		private int _month;
		[SerializeField]
		private int _day;
	}
}