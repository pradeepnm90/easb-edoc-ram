import {HttpActionType} from "../../app.config";
export class EntityApiLink {
  //"RelatedEntity", "ReferenceEntity", "SubEntity", "Action"
  type: string;
  //related entity name
  rel: string;
  href: string;
  //enum array, ["GET", "PUT"]
  method: HttpActionType;

}
