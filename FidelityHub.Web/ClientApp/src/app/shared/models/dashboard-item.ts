export class DashboardItem {
  constructor(
    public title: string,
    public cols: number = null,
    public rows: number = null,
    public route: string = null
  ) {}
}
