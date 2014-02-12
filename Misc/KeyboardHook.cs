/*
* Copyright (c) 2013-2014 Dobrescu Andrei
* 
* This file is part of Universal Chevereto Uploadr.
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Universal Chevereto Uploadr. If not, see http://www.gnu.org/licenses/.
*/

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Universal_Chevereto_Uploadr.KeyboardHook
{
	//classes taked from http://www.liensberger.it/web/blog/?p=207
	
	// Represents the window that is used internally to get the messages.
	class Window : NativeWindow, IDisposable
	{
       	public Window ()
       	{
           	this.CreateHandle (new CreateParams ());
       	}
       	
       	protected override void WndProc (ref Message m)
       	{
           	base.WndProc (ref m);
	    	if (m.Msg==0x0312)
           	{
		        Keys key=(Keys)(((int)m.LParam>>16)&0xFFFF);
              	ModifierKeys modifier=(ModifierKeys)((int)m.LParam&0xFFFF);
	                if (KeyPressed!=null)
                	    KeyPressed (this, new KeyPressedEventArgs (modifier, key));
            	}
        	}
        	
        	public event EventHandler <KeyPressedEventArgs> KeyPressed;
	
        	public void Dispose ()
	        {
	            this.DestroyHandle ();
	        }
    	}
	}
		
	public class KeyPressedEventArgs : EventArgs
	{
    	private ModifierKeys _modifier;
	    private Keys _key;

	    internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
   		{
       		_modifier = modifier;
    	    _key = key;
   		}

   		public ModifierKeys Modifier
   		{
   		    get { return _modifier; }
   		}

   		public Keys Key
   		{
   		    get { return _key; }
   		}
	}
	
	[Flags]
	public enum ModifierKeys : uint
	{
		None=0,
    	Alt=1,
	    Control=2,
    	Shift=4,
	}
	
	public sealed class cKeyboardHook : IDisposable
	{
		[DllImport ("user32.dll")] private static extern bool RegisterHotKey (IntPtr hWnd, int id, uint fsModifiers, uint vk);
		[DllImport ("user32.dll")] private static extern bool UnregisterHotKey (IntPtr hWnd, int id);
		
		private Universal_Chevereto_Uploadr.KeyboardHook.Window _window = new Universal_Chevereto_Uploadr.KeyboardHook.Window ();
	    private int _currentId;
	    
	    public cKeyboardHook()
   		{
        	_window.KeyPressed += delegate(object sender, KeyPressedEventArgs args)
        	{
            	if (KeyPressed != null) KeyPressed (this, args);
        	};
    	}
	    
	    //returns false if the operation was not successful
	    public bool RegisterHotKey (ModifierKeys modifier, Keys key)
    	{
	        _currentId = _currentId + 1;
	        return (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key));
    	}
    	
    	public event EventHandler<KeyPressedEventArgs> KeyPressed;
    	
	    public void Dispose()
    	{
        	for (int i = _currentId; i > 0; i--)
       		{
            	UnregisterHotKey (_window.Handle, i);
        	}
        	_window.Dispose();
    	}
	}
