import { ChangeDetectorRef, Component, ComponentFactoryResolver, Input, OnInit, ViewContainerRef } from '@angular/core';

@Component({
  selector: 'app-component-host',
  templateUrl: './component-host.component.html',
  styleUrls: ['./component-host.component.css']
})
export class ComponentHostComponent implements OnInit {

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private viewContainerRef: ViewContainerRef,
    private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit() {
  }

  @Input()
  set component(component: any) {
    this.viewContainerRef.clear();
    if (component) {
      const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);
      const componentRef = this.viewContainerRef.createComponent(componentFactory);
    }

  }

}
