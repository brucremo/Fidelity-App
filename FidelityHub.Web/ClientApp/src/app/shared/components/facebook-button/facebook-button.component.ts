import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'cool-facebook-button',
  templateUrl: './facebook-button.component.html',
  styleUrls: ['./facebook-button.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FacebookButtonComponent implements OnInit {

  constructor() { }

  @Input()
  public disabled: boolean;

  @Input()
  public color: 'dark' | 'light' = 'dark';

  @Output()
  public click: EventEmitter<MouseEvent> = new EventEmitter<MouseEvent>();

  ngOnInit() {
  }

  public onClick(event: MouseEvent) {
    if (this.disabled) {
      return;
    }

    this.click.emit(event);

    event.stopPropagation();
    event.preventDefault();
  }

}
