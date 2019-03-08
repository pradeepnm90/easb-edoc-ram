import {TestBed, ComponentFixture} from '@angular/core/testing';
import {Component, DebugElement, Output, EventEmitter} from "@angular/core";
import {By} from "@angular/platform-browser";
import {KeyboardKeyDirective} from './keyboard-key.directive';

@Component({
  template: `
    <div keyboard-key></div>`
})
class TestKeyboardKeyComponent {

}

describe('Directive: KeyboardkeyEvent', () => {
  let component: TestKeyboardKeyComponent;
  let fixture: ComponentFixture<TestKeyboardKeyComponent>;
  let inputEl: DebugElement;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TestKeyboardKeyComponent, KeyboardKeyDirective]
    });
    fixture = TestBed.createComponent(TestKeyboardKeyComponent);
    component = fixture.componentInstance;
    inputEl = fixture.debugElement.query(By.css('div'));
  });

  it('keyboard key down event', () => {
    const customEvent: any = document.createEvent('CustomEvent');
    customEvent.keyCode = 17;
    inputEl.triggerEventHandler('keydown', null);
    inputEl.triggerEventHandler('keydown', customEvent);
    fixture.detectChanges();
    expect(customEvent.keyCode).toEqual(17);
  });

  it('keyboard key up event', () => {
    const customEvent: any = document.createEvent('CustomEvent');
    customEvent.keyCode = 17;
    inputEl.triggerEventHandler('keyup', null);
    inputEl.triggerEventHandler('keyup', customEvent);
    fixture.detectChanges();
    expect(customEvent.keyCode).toEqual(17);
  });
});
