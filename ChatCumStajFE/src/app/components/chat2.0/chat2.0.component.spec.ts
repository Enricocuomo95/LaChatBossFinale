import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Chat20Component } from './chat2.0.component';

describe('Chat20Component', () => {
  let component: Chat20Component;
  let fixture: ComponentFixture<Chat20Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Chat20Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(Chat20Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
