import { Directive, ElementRef, Output, EventEmitter, HostListener} from '@angular/core';
@Directive({
  selector: "[keyboard-key]"
})
export class KeyboardKeyDirective {
  @Output() onKeyboardUpEvent: EventEmitter<any> = new EventEmitter();
  @Output() onKeyboardDownEvent: EventEmitter<any> = new EventEmitter();

  constructor(private el: ElementRef) { }

  @HostListener('keydown', ['$event'])
  handleKeyboardDownKeyEvent(event: KeyboardEvent) {
    if (event && event.keyCode === 17) {
      this.onKeyboardDownEvent.emit(event);
    }
  }

  @HostListener('keyup', ['$event'])
  handleKeyboardUpKeyEvent(event: KeyboardEvent) {
    if (event && event.keyCode === 17) {
      this.onKeyboardUpEvent.emit(event);
    }
  }
}
