using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LoLCameraSharp.MemoryFunctions
{
    public class MemoryEditor
    {
        #region Process Access Consts
        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;
        #endregion

        #region Imports
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
          int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress,
          byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);
        #endregion

        #region Public & Private Vars
        public bool gameFound { get { return _gameFound; } set { _gameFound = value; } }
        public IntPtr processHandler { get { return _processHandle; } }
        public IntPtr baseModule { get { return _baseModule; } }
        public int moduleSize { get { return _moduleSize; } }
        
        private bool _gameFound = false;
        private IntPtr _processHandle;
        private IntPtr _baseModule;
        private int _moduleSize;
        #endregion

        #region Constructors
        public MemoryEditor()
        {
        }

        public MemoryEditor(string gameName)
        {
            FindGame(gameName);
        }
        #endregion

        #region Find Game Funcs
        public bool FindGame(string gameName)
        {
            try
            {
                Process[] processes = Process.GetProcessesByName(gameName);
                _gameFound = false;
                if (processes.Count() != 0)
                {
                    this._processHandle = OpenProcess(PROCESS_WM_READ | PROCESS_VM_OPERATION | PROCESS_VM_WRITE, false, processes[0].Id);
                    this._baseModule = processes[0].MainModule.BaseAddress;
                    this._moduleSize = processes[0].MainModule.ModuleMemorySize;
                    _gameFound = true;
                }
                return _gameFound;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region ReadMemory Funcs
        public Int32 ReadInt(IntPtr address)
        {
            return BitConverter.ToInt32(ReadBytes(address, sizeof(Int32)), 0);
        }

        public uint ReadUInt(IntPtr address)
        {
            return BitConverter.ToUInt32(ReadBytes(address, sizeof(Int32)), 0);
        }

        public float ReadFloat(IntPtr address)
        {
            return BitConverter.ToSingle(ReadBytes(address, sizeof(float)), 0);
        }

        public byte[] ReadBytes(IntPtr address, int size)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[size];
            _gameFound = ReadProcessMemory((int)_processHandle, (int)address, buffer, buffer.Length, ref bytesRead);
            return buffer;
        }
        #endregion

        #region WriteMemory Funcs
        public void WriteFloat(IntPtr address, float value)
        {
            WriteBytes(address, BitConverter.GetBytes(value));
        }

        public void WriteBytes(IntPtr address, byte[] buffer)
        {
            int bytesWritten = 0;
            _gameFound = WriteProcessMemory((int)_processHandle, (int)address, buffer, buffer.Length, ref bytesWritten);
        }
        #endregion
    }
}
