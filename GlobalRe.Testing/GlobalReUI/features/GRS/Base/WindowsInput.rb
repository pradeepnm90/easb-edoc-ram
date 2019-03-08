require 'watir'
require 'Win32API'
module  WindowsInput
  SetCursorPos = Win32API.new('user32', 'SetCursorPos', 'II', 'I')
  SendInput = Win32API.new('user32', 'SendInput', 'IPI', 'I')
  INPUT_MOUSE = 0
  MOUSEEVENTF_LEFTDOWN = 0x0002
  MOUSEEVENTF_LEFTUP = 0x0004

  module_function

  def send_input(inputs)
    n = inputs.size
    ptr = inputs.collect {|i| i.to_s}.join
    SendInput.call(n, ptr, inputs[0].size)
  end

  def create_mouse_input(mouse_flag)
    mi = Array.new(7,0)
    mi[0] = INPUT_MOUSE
    mi[4] = mouse_flag
    mi.pack('LLLLLLL')
  end

  def move_mouse(x, y)
    SetCursorPos.call(x,y)
  end

  def left_click
    send_input([leftdown_event, leftup_event])
  end

  def left_down
    send_input([leftdown_event])
  end

  def left_up
    send_input([leftup_event])
  end

  def leftup_event
    create_mouse_input(MOUSEEVENTF_LEFTUP)
  end

  def leftdown_event
    create_mouse_input(MOUSEEVENTF_LEFTDOWN)
  end
end