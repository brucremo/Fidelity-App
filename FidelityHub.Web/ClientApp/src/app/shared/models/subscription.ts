export class Subscription {
  id: number;
  description: string;
  recurrenceDays: number;
  price: number;
  title: string;
  active: boolean;

  constructor() {
    this.id = -1;
  }
}
