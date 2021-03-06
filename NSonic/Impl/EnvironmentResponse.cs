﻿using System;

namespace NSonic.Impl
{
    struct EnvironmentResponse
    {
        public int Protocol { get; }
        public int Buffer { get; }
        public int MaxBufferStringLength { get; }

        public EnvironmentResponse(int protocol, int buffer)
        {
            this.Protocol = protocol;
            this.Buffer = buffer;
            this.MaxBufferStringLength = (int)Math.Floor((buffer * 0.5) / 4);
        }
    }
}
