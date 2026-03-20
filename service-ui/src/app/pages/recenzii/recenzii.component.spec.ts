import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecenziiComponent } from './recenzii.component';

describe('RecenziiComponent', () => {
  let component: RecenziiComponent;
  let fixture: ComponentFixture<RecenziiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecenziiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecenziiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
