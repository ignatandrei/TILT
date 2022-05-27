const JANUARY:number = 0;
const THURSDAY:number = 4;  

export class TILT {
  constructor(tilt: Partial<TILT> | null = null) {
    if (tilt != null) {
      // Object.keys(tilt).forEach((key) => {
      //   (this as any)[key] = (tilt as any)[key];
      // });
      Object.assign(this,tilt);
    }
  }
  public id?: number;
  public idurl?: number;
  public text?: string | null;
  public link?: string | null;
  public forDate?: string | null;
  public existsPrev?: boolean | null ;
  public existsNext?: boolean | null ;
  public numberOfDays: number =1;
  public isMax : boolean=false;
  public isPartOfMax : boolean=false;
  public MaxDaysInStreak:number=1;
  public prevTilt: TILT | null = null;
  public get TheDate(): Date {
    return new Date(this.forDate! + 'Z');
  }
  public get LocalDateString(): string {
    return this.TheDate.toLocaleString();
  }
  

  public get LocalJustDate(): Date {
    var d = this.TheDate;
    d.setHours(0);
    d.setMinutes(0);
    d.setSeconds(0);
    d.setMilliseconds(0);
    return d;
  }

  public get NextJustDate(): Date {
    var d = this.LocalJustDate;
    d.setDate(d.getDate() + 1);
    return d;
  }
  public get PrevJustDate(): Date {
    var d = this.LocalJustDate;
    d.setDate(d.getDate() - 1);
    return d;
  }
  public get WeekNumber(): number 
  {
    return this.week(this.LocalJustDate);  

  }
//shameless copy from
//https://github.com/angular/angular/blob/3a60063a54d850c50ce962a8a39ce01cfee71398/packages/common/src/i18n/format_date.ts#L408
private week(date: Date):number {
    const thisThurs = this.getThursdayThisWeek(date);
    // Some days of a year are part of next year according to ISO 8601.
    // Compute the firstThurs from the year of this week's Thursday
    const firstThurs = this.getFirstThursdayOfYear(thisThurs.getFullYear());
    const diff = thisThurs.getTime() - firstThurs.getTime();
    var result = 1 + Math.round(diff / 6.048e8);  // 6.048e8 ms per week
    return result;
  }
  private  getThursdayThisWeek(datetime: Date) {
    return this.createDate(
        datetime.getFullYear(), datetime.getMonth(),
        datetime.getDate() + (THURSDAY - datetime.getDay()));
  }
  private getFirstThursdayOfYear(year: number) {
    const firstDayOfYear = this.createDate(year, JANUARY, 1).getDay();
    return this.createDate(
        year, 0, 1 + ((firstDayOfYear <= THURSDAY) ? THURSDAY : THURSDAY + 7) - firstDayOfYear);
  }
  private createDate(year: number, month: number, date: number): Date {
    // The `newDate` is set to midnight (UTC) on January 1st 1970.
    // - In PST this will be December 31st 1969 at 4pm.
    // - In GMT this will be January 1st 1970 at 1am.
    // Note that they even have different years, dates and months!
    const newDate = new Date(0);
  
    // `setFullYear()` allows years like 0001 to be set correctly. This function does not
    // change the internal time of the date.
    // Consider calling `setFullYear(2019, 8, 20)` (September 20, 2019).
    // - In PST this will now be September 20, 2019 at 4pm
    // - In GMT this will now be September 20, 2019 at 1am
  
    newDate.setFullYear(year, month, date);
    // We want the final date to be at local midnight, so we reset the time.
    // - In PST this will now be September 20, 2019 at 12am
    // - In GMT this will now be September 20, 2019 at 12am
    newDate.setHours(0, 0, 0);
  
    return newDate;
  }
  
  


}
