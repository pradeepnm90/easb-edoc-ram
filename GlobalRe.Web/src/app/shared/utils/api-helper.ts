import {ApiLinkType, HttpActionType} from "../../app.config";
import {EntityApiLink} from "../models/entity-api-link";
export class ApiHelper {
    static getEntitySelfGetApiUrl(entityLinks: EntityApiLink[]){
        if (entityLinks != null) {
          const entityGetLinks = entityLinks.filter(s => s.type === ApiLinkType.entity && s.rel === ApiLinkType.self && s.method === HttpActionType.get);
            if (entityGetLinks != null && entityGetLinks.length === 1) {
                return entityGetLinks[0].href;
            }
        }
        return null;
    }
    static getSubEntityApiUrl(entityLinks: EntityApiLink[], subEntityName: string) {

        if (entityLinks != null) {
          const subEntityLinks = entityLinks.filter(s => s.type === ApiLinkType.subEntity && s.rel === subEntityName);
            if (subEntityLinks != null && subEntityLinks.length === 1) {
                return subEntityLinks[0].href;
            }
        }
        return null;
    }

    static getEntityActionApiUrl(entityLinks: EntityApiLink[], actionType: HttpActionType, actionTargetEntityName: string) {

        if (entityLinks != null) {

          const actionLink = entityLinks.filter(s => s.type === ApiLinkType.action
                                                                && s.method === actionType
                                                                && s.rel === actionTargetEntityName);
            if (actionLink != null && actionLink.length === 1) {
                return actionLink[0].href;
            }
        }
        return null;
    }

    static parseErrResponse(responseBody: any): any {
        try {
            if (responseBody._body != null) {
                const responseObj = JSON.parse(responseBody._body);
                if (responseObj.messages != null && responseObj.messages.length > 0)
                    return responseObj.messages;
            }
            return JSON.parse(responseBody);
        } catch (e) {
            return responseBody;
        }
    }

    static getAvailableActionButtons(entityLinks: EntityApiLink) {

    }
}
