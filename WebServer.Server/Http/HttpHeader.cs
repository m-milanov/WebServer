﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Common;

namespace WebServer.Server.Http
{
    public class HttpHeader
    {
        public const string ContentType = "Content-Type";
        public const string ContentLenght = "Content-Lenght";
        public const string Server = "Server";
        public const string Date = "Date";
        public const string Location = "Location";
        public HttpHeader(string name, string value)
        {
            Guard.AgainstNull(name, nameof(name));
            Guard.AgainstNull(value, nameof(value));

            this.Name = name;
            this.Value = value;
        }
        public string Name { get; init; }

        public string Value { get; set; }

        public override string ToString()
            => $"{this.Name}: {this.Value}";
        
    }
}
