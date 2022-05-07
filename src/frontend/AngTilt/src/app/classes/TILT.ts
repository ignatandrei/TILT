export class TILT {


    constructor(tilt: Partial<TILT>| null = null) {
      if(tilt != null){
        Object.keys(tilt).forEach(key =>  {
           (this as any)[key] = (tilt as any)[key];
        });

      }
      
    }
    public id?: number;
    public idurl?: number;
    public text?: string | null;
    public link?: string | null;
    public forDate?: string | null;

    public get TheDate(): Date{
      return new Date(this.forDate!+'Z');
    }
    public get LocalDateString(): string{
      return this.TheDate.toLocaleString();
    }
    public get LocalDate(): Date{
      return new Date(this.LocalDateString);
    }
  }