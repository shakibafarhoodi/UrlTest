﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
	public interface IViewRenderService
	{
		Task<string> RenderToStringAsync(string viewName, object model);

	}
}
