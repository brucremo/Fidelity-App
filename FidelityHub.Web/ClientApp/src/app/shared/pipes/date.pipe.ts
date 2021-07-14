import { Pipe, PipeTransform } from '@angular/core';
import { DateTime } from "luxon";

@Pipe({
  name: 'date'
})
export class DatePipe implements PipeTransform {

  transform(value: Date, format: string): string {
    return DateTime.fromISO(value.toString()).toFormat(format).toLocaleLowerCase("br");
  }

}
