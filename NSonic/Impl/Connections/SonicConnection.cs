﻿using NSonic.Utils;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NSonic.Impl.Connections
{
    abstract class SonicConnection : ISonicConnection
    {
        private readonly ISonicSessionFactoryProvider sessionFactoryProvider;
        private readonly string hostname;
        private readonly int port;
        private readonly string secret;

        protected SonicConnection(ISonicSessionFactoryProvider sessionFactoryProvider
            , ISonicRequestWriter requestWriter
            , string hostname
            , int port
            , string secret
            )
        {
            this.sessionFactoryProvider = sessionFactoryProvider;
            this.RequestWriter = requestWriter;
            this.hostname = hostname;
            this.port = port;
            this.secret = secret;

            // Default environment.
            this.Environment = new EnvironmentResponse(1, 20000);
        }

        protected abstract string Mode { get; }

        protected ISonicRequestWriter RequestWriter { get; }
        protected ISonicSessionFactory SessionFactory { get; private set; }

        public EnvironmentResponse Environment { get; private set; }

        public void Connect()
        {
            this.SessionFactory = this.sessionFactoryProvider.Create(hostname, port);

            using (var session = this.SessionFactory.Create(this.Environment))
            {
                this.Environment = this.RequestWriter.WriteStart(session, this.Mode, this.secret);
            }
        }

        public void Dispose()
        {
            if (this.SessionFactory != null)
            {
                using (var session = this.SessionFactory.Create(this.Environment))
                {
                    try
                    {
                        session.Write("QUIT");
                        Assert.IsTrue(session.Read().StartsWith("ENDED"), "Quit failed when disposing sonic connection");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }

                this.SessionFactory.Dispose();
            }
        }
    }
}
