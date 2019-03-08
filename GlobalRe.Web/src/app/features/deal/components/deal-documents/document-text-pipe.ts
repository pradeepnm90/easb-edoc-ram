import { Pipe, PipeTransform } from "@angular/core";
import { strictEqual } from "assert";

@Pipe({
  name: "truncateDocTextCharacters"
})
export class TruncateDocumentTextPipe implements PipeTransform {
  transform(value: any, limit: number) {
    if (value.length >limit) {
      return (
        value.substr(0, limit-10) +
        "... "+
        value.substr(value.length - 10, value.length)
      );
    }
    return value;
  }
}
