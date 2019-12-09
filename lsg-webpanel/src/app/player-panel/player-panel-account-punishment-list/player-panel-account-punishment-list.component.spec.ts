/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PlayerPanelAccountPunishmentListComponent } from './player-panel-account-punishment-list.component';

describe('PlayerPanelAccountPunishmentListComponent', () => {
  let component: PlayerPanelAccountPunishmentListComponent;
  let fixture: ComponentFixture<PlayerPanelAccountPunishmentListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerPanelAccountPunishmentListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerPanelAccountPunishmentListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
