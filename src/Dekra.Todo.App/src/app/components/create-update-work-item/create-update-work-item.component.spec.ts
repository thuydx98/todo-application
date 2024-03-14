import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateWorkItemComponent } from './create-update-work-item.component';

describe('CreateUpdateWorkItemComponent', () => {
  let component: CreateUpdateWorkItemComponent;
  let fixture: ComponentFixture<CreateUpdateWorkItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreateUpdateWorkItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateUpdateWorkItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
