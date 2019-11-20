using System;
using System.Collections.Generic;
using System.Text;

namespace EcoHelper.Application.DTO.Garbage.Commands
{
    public class CreateGarbageRequest
    {
        public string Name { get; set; }
        public int DumpsterId { get; set; }
    }
}
