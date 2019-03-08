import {EntityApiLink} from "./entity-api-link";
import {EntityType} from "../../app.config";

export class EntityApiData<T> {
    data: T;
    links: EntityApiLink[];
    url?: string;
    messages?: any[];
    entityType?: EntityType;
}