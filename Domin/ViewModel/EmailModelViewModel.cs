using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Model
{
	public class EmailModelViewModel
	{
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public EmailModelViewModel(string to, string subject, string body)
		{
			To = to;
			Subject = subject;
			Body = body;
		}

		
	}

}

