﻿using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;

namespace Tranquility.Helpers
{
    public class MouseProxy
    {
        private readonly SerialPort _port;
        public MouseProxy()
        {
            var ports = SerialPort.GetPortNames();
            _port = new SerialPort(ports[1], 9600);
            _port.Open();
        }

        public void Fire()
        {
            var bytes = new byte[] { 0 };
            _port.Write(bytes, 0, 1);
        }

        public void Move(int x, int y)
        {
            _port.Write(new []{(byte)x, (byte)y}, 0, 2);
        }

        public void Close()
        {
            _port.Close();
        }
    }
}