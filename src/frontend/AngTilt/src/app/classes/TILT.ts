export class TILT {
  constructor(tilt: Partial<TILT> | null = null) {
    if (tilt != null) {
      Object.keys(tilt).forEach((key) => {
        (this as any)[key] = (tilt as any)[key];
      });
    }
  }
  public id?: number;
  public idurl?: number;
  public text?: string | null;
  public link?: string | null;
  public forDate?: string | null;
  public existsPrev?: boolean | null ;
  public existsNext?: boolean | null ;
  

  public get TheDate(): Date {
    return new Date(this.forDate! + 'Z');
  }
  public get LocalDateString(): string {
    return this.TheDate.toLocaleString();
  }
  public get LocalDate(): Date {
    return new Date(this.LocalDateString);
  }

  public get LocalJustDate(): Date {
    var d = this.LocalDate;
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
}
