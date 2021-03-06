﻿using UnityEngine;

namespace Account
{
    public class MyAccount : Singleton<MyAccount>
    {
        private string _ign;
        private string _token;
        private bool _isAccountSet = false;
        private ushort? _currentRoom = null;
        private int? _currentInstance = null;
        
        /* Cached inventory, friends etc */
        
        protected MyAccount() {}

        public bool Initialize(string ign, string token)
        {
            if (_isAccountSet) return false;
            
            _ign = ign;
            _token = token;
            return _isAccountSet = true;
        }

        public string GetIGN() { return _ign; }
        public string GetToken() { return _token; }
        public ushort? GetCurrentRoom() { return _currentRoom; }
        public void SetCurrentRoom(ushort? value) { _currentRoom = value; }
        public void SetCurrentInstance(int? value) { _currentInstance = value; }

        public void Deinitialize()
        {
            _ign = null;
            _token = null;
            _isAccountSet = false;
        }

        ~MyAccount()
        {
            Debug.Log("MyAccount instance is removed.");
            Deinitialize();
        }
    }
}
