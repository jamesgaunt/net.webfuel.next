import { Overlay, OverlayConfig, OverlayRef, PositionStrategy } from "@angular/cdk/overlay";
import { ComponentPortal, ComponentType, TemplatePortal } from "@angular/cdk/portal";
import { ElementRef, Injectable, TemplateRef, ViewContainerRef } from "@angular/core";
import _ from '../shared/underscore';

export interface IOverlayOptions {
  // The width of the overal panel. If a number is provided, pixel units are assumed. 
  width?: number | string;
  // The height of the overal panel. If a number is provided, pixel units are assumed.
  height?: number | string;
  // The min-width of the overal panel. If a number is provided, pixel units are assumed.
  minWidth?: number | string;
  // The min-height of the overal panel. If a number is provided, pixel units are assumed.
  minHeight?: number | string;
  // The max-width of the overal panel. If a number is provided, pixel units are assumed.
  maxWidth?: number | string;
  // The max-height of the overal panel. If a number is provided, pixel units are assumed.
  maxHeight?: number | string;
  // Element the overaly is connected to
  connectedTo?: ElementRef<any>;
  // Does the overlay have a backdrop 
  hasBackdrop?: boolean;
  // Class to add to the backdrop
  backdropClass?: string;
  // Should the overlay close automatically if the backdrop is clicked 
  closeOnBackdropClick?: boolean;
  // Class to add to the panel
  panelClass?: string;
  // Position strategy
  positionStrategy?: PositionStrategy;
  // Specified Position
  positionX?: number;
  positionY?: number;
}

const DEFAULT_POPUP_SETTINGS: IOverlayOptions = {
  width: undefined,
  height: undefined,
  panelClass: "popup-panel",
  hasBackdrop: true,
  closeOnBackdropClick: true
}

@Injectable()
export class OverlayService {

  constructor(
    private overlay: Overlay) {
  }

  openTemplate(template: TemplateRef<unknown>, viewContainerRef: ViewContainerRef, options?: IOverlayOptions): OverlayRef {
    const overlayRef = this._buildOverlayRef(options || {});
    const portal = new TemplatePortal(template, viewContainerRef);
    overlayRef.attach(portal);
    return overlayRef;
  }

  private _buildOverlayRef(options: IOverlayOptions) {
    options = this._buildOptions(options);
    const overlayConfig: OverlayConfig = {
      width: options.width,
      height: options.height,
      hasBackdrop: options.hasBackdrop,
      backdropClass: options.backdropClass,
      panelClass: options.panelClass,
      positionStrategy: options.positionStrategy,
      scrollStrategy: this.overlay.scrollStrategies.noop()
    }
    var overlayRef = this.overlay.create(overlayConfig);
    return overlayRef;
  }

  private _buildOptions(options?: IOverlayOptions): IOverlayOptions {
    options = _.merge(DEFAULT_POPUP_SETTINGS, options);
    options.positionStrategy = options.positionStrategy || this.overlay.position().flexibleConnectedTo(options.connectedTo!).withPositions([
      { originX: 'start', originY: 'bottom', overlayX: 'start', overlayY: 'top' },
      { originX: 'start', originY: 'top', overlayX: 'start', overlayY: 'bottom' },
      { originX: 'end', originY: 'bottom', overlayX: 'end', overlayY: 'top' },
      { originX: 'end', originY: 'top', overlayX: 'end', overlayY: 'bottom' },
    ]).withFlexibleDimensions(true).withPush(false);
    return options;
  }

}

