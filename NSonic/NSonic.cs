﻿using NSonic.Impl;
using NSonic.Impl.Connections;

namespace NSonic
{
    public static class NSonic
    {
        private static ISonicSessionFactoryProvider SessionFactoryProvider { get; set; }

        static NSonic()
        {
            SessionFactoryProvider = new SonicSessionFactoryProvider();
        }

        public static ISonicControlConnection Control(string hostname
            , int port
            , string secret
            )
        {
            return new SonicControlConnection(SessionFactoryProvider, hostname, port, secret);
        }

        public static ISonicIngestConnection Ingest(string hostname
            , int port
            , string secret
            )
        {
            return new SonicIngestConnection(SessionFactoryProvider, hostname, port, secret);
        }

        public static ISonicSearchConnection Search(string hostname
            , int port
            , string secret
            )
        {
            return new SonicSearchConnection(SessionFactoryProvider, hostname, port, secret);
        }
    }
}