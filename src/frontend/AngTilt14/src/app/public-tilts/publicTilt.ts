export class publicTilt {
  
  constructor(copyData: Partial<publicTilt> | null = null) {
    if (copyData != null) {
      // Object.keys(copyData).forEach((key) => {
      //   (this as any)[key] = (copyData as any)[key];
      // });
      Object.assign(this,copyData);
    }
  }
  public url: string = 'please wait';
  public nrTilts: number | null = null;

}
