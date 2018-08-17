﻿using System;
using Newtonsoft.Json;
using UnityEngine;

public partial class MyGame
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GameTime : SerializeManager, HuangDAPI.GMDate
	{
        public static void Initialize()
        {
            current = new GameTime();

            current.incMonthEvent += Economy.UpDate;
        }

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
            int day = 0;
            int month = 0;
            int year = 0;

            if(c1.day < c2.day)
            {
                day = c1.day + 30 - c2.day;
                month = c1.month - 1;
            }
            else
            {
                day = c1.day - c2.day;
                month = c1.month;
            }

            if(month < c2.month)
            {
                month = (month + 12 - c2.month);
                year = c1.year -1;
            }
            else
            {
                month = (month - c2.month);
                year = c1.year;
            }

            if(year < c2.year)
            {
                throw new ArgumentException(c1 + "is earlier than" + c2);
            }
            else
            {
                year = year - c2.year;
            }

            int result = year * 30*12 + month * 30 + day;

            return result;
        }


        public event Action incDayEvent;
        public event Action incMonthEvent;
        public event Action incYearEvent;

 
        [JsonProperty]
		private int _year;
        [JsonProperty]
		private int _month;
        [JsonProperty]
		private int _day;

        [SerializeField]
        public static GameTime current;
	}
}