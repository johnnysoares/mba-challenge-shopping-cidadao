﻿using System.IO;
using System.Text;

namespace UTILCommon.Extensions.Xml.Entities {

	public class StringWriterWithEncoding : StringWriter {

		private readonly Encoding encoding;

		public StringWriterWithEncoding(){ }

		public StringWriterWithEncoding(Encoding encoding){
			this.encoding = encoding;
		}

		public override Encoding Encoding {
			get { return encoding; }
		}

	}
}