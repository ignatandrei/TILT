import { Pipe, PipeTransform } from '@angular/core';
import { formatDistance, subDays } from 'date-fns'
@Pipe({name: 'formatDate'})
export class FormatDatePipe implements PipeTransform {
  transform(value: string| null| undefined): string {
    if(value == null) return "";
    if(value == undefined) return "";
    
    var date= new Date(value+'Z');

    return formatDistance(date, new Date(), { addSuffix: true });
  }
}