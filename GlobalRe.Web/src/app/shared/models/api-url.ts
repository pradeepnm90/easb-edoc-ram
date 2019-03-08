export class ApiUrl{
  key:string;
  apiUrl:string;
  constructor(target:{key:string;
    url:string;})
{
  this.key=target.key;
  this.apiUrl=target.url;
}
}
