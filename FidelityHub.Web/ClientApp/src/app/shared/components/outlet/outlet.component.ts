import { Component, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { WindowOrientation, WindowLayout } from '../../models/window';
import { map, shareReplay } from 'rxjs/operators';
import { ComponentPortal } from '@angular/cdk/portal';
import { OverlayPortalComponent } from '../overlay-portal/overlay-portal.component';
import { OverlayRef, Overlay, OverlayConfig } from '@angular/cdk/overlay';
import { LoadingService } from '../../services/loading.service';

@Component({
  selector: 'app-outlet',
  templateUrl: './outlet.component.html',
  styleUrls: ['./outlet.component.css']
})
export class OutletComponent {

  constructor(
    public breakPointObserver: BreakpointObserver
  ) {
  }

  public isXSmall = this.breakPointObserver.observe([
    Breakpoints.XSmall,
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isSmall = this.breakPointObserver.observe([
    Breakpoints.Small,
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isMedium = this.breakPointObserver.observe([
    Breakpoints.Medium,
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isLarge = this.breakPointObserver.observe([
    Breakpoints.Large,
  ]).subscribe(result => {
    if (result.matches) {
      return true;
    }else{
      return false;
    }
  });

  public isXLarge = this.breakPointObserver.observe([
    Breakpoints.XLarge,
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isHandset = this.breakPointObserver.observe([
    Breakpoints.HandsetLandscape,
    Breakpoints.HandsetPortrait
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isTablet = this.breakPointObserver.observe([
    Breakpoints.TabletLandscape,
    Breakpoints.TabletPortrait
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isWeb = this.breakPointObserver.observe([
    Breakpoints.WebLandscape,
    Breakpoints.WebPortrait
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isPortrait = this.breakPointObserver.observe([
    Breakpoints.TabletPortrait,
    Breakpoints.HandsetPortrait,
    Breakpoints.WebPortrait
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public isLandscape = this.breakPointObserver.observe([
    Breakpoints.HandsetLandscape,
    Breakpoints.TabletLandscape,
    Breakpoints.WebLandscape
  ]).pipe(
    map(result => result.matches),
    shareReplay()
  );

  public showOverlay(overlay: Overlay, config: OverlayConfig = null): void {
    var overlayRef: OverlayRef;
    if(config == null){
      overlayRef = overlay.create();
    }else{
      overlayRef = overlay.create(config);
    }
    const userProfilePortal = new ComponentPortal(OverlayPortalComponent);
    overlayRef.attach(userProfilePortal);
  }
}
