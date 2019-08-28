﻿using System;

namespace ProjectEulerCS.Problems
{
	public class Problem : Attribute
	{
		public ProblemState State { get; set; }

		public Problem(ProblemState state)
		{
			State = state;
		}
	}
}