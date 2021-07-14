export class RequestResult {
  code: number;
  message: string;
  timestamp: Date;
  data: any;

  constructor(code: number, message: string, data: any){
    this.timestamp = new Date();
    this.message = message;
    this.code = code;
    this.data = data;
  }
}
