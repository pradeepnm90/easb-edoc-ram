import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import "rxjs/add/observable/of";

@Injectable()
export class DocumentService {
  docJson = {
    ID: "447123005",
    Name: "Ironshore 2014 Bond-Credit-Mortgage-Surety QS",
    Container: "File",
    Contents: [
      {
        ID: "447123009",
        Name: "Final Documents",
        Container: "Folder",
        Contents: [
          {
            ID: "447123014",
            Name: "BINDER 103324-1-1.PDF",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447211374",
            Name: "BINDER 103324-1-1.PDF",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447436436",
            Name: "#103324 OUR SIGNED SLIP 2014 (12/31/13)",
            Container: "Document",
            Contents: null,
            Metadata: null
          }
        ],
        Metadata: [
          {
            Name: "folder type",
            Value: "Final Documents",
            ContainerLevel: "Folder"
          },
          {
            Name: "file number",
            Value: "103324",
            ContainerLevel: "File"
          }
        ]
      },
      {
        ID: "447330716",
        Name: "Submission",
        Container: "Folder",
        Contents: [
          {
            ID: "447330718",
            Name:
              "FW  IRONSHORE CREDIT & PRI - 2014 REINSURANCE RENEWAL; BURRER, JOSCELIN; KONIKOWSKI, ELLEN",
            Container: "Document",
            Contents: null,
            Metadata: null
          }
        ],
        Metadata: [
          {
            Name: "folder type",
            Value: "Submission",
            ContainerLevel: "Folder"
          },
          {
            Name: "file number",
            Value: "103324",
            ContainerLevel: "File"
          }
        ]
      },
      {
        ID: "447368971",
        Name: "Contract Docs",
        Container: "Folder",
        Contents: [
          {
            ID: "447368974",
            Name:
              "FW  IRONSHORE 2014 CREDIT AND POLITICAL RISKS PROGRAMME RENEWAL; BURRER, JOSCELIN; KONIKOWSKI, ELLEN",
            Container: "Document",
            Contents: null,
            Metadata: null
          }
        ],
        Metadata: [
          {
            Name: "folder type",
            Value: "Contract Docs",
            ContainerLevel: "Folder"
          },
          {
            Name: "file number",
            Value: "103324",
            ContainerLevel: "File"
          }
        ]
      },
      {
        ID: "447405274",
        Name: "Correspondence",
        Container: "Folder",
        Contents: [
          {
            ID: "447405278",
            Name: "IRONSHORE (2); KONIKOWSKI, ELLEN; BURRER, JOSCELIN",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447464090",
            Name:
              "FW: IRONSHORE SPECIALTY - POLITICAL RISK PNOC; BURRER, JOSCELIN; KONIKOWSKI, ELLEN",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447488594",
            Name:
              "FW: IRONSHORE SPECIALTY - POLITICAL RISK PNOC; BURRER, JOSCELIN; KONIKOWSKI, ELLEN",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447510669",
            Name:
              'MESSAGE FROM "RNP0026737F7DCC"; EMERALD@ALTERRA-US.COM; KONIKOWSKI, ELLEN',
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447531421",
            Name:
              'MESSAGE FROM "RNP0026737F7DCC"; EMERALD@ALTERRA-US.COM; KONIKOWSKI, ELLEN',
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "502422548",
            Name: "Correspondence RI",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "502422549",
            Name: "Correspondence RI",
            Container: "Document",
            Contents: null,
            Metadata: null
          }
        ],
        Metadata: [
          {
            Name: "folder type",
            Value: "Correspondence",
            ContainerLevel: "Folder"
          },
          {
            Name: "file number",
            Value: "103324",
            ContainerLevel: "File"
          }
        ]
      },
      {
        ID: "447561652",
        Name: "Claims Related",
        Container: "Folder",
        Contents: [
          {
            ID: "447561653",
            Name:
              "CONNERS, KERRY; BURRER, JOSCELIN; IRONSHORE TRADE CREDIT AND PRI CLAIM REPORT",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447583610",
            Name:
              "REVIEWED BLC; LUCAS-CHIN, BARBARA; NIGEL.MUNSON@WILLISTOWERSWATSON.COM; SPECIALTY-RI (SPECIALTY-RI@WILLISTOWERSWATSON.COM); FW: REASSURED: IRONSHORE SPECIALTY INSURANCE COMPANY - POLICY DES: POLITICAL RISK QUOTA SHARE REINSURANCE CONTRACT - U/W REF: TBA -",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447603287",
            Name:
              "REVIEWED BLC; FLORENCIO, JERSON; LUCAS-CHIN, BARBARA; FW: REASSURED: IRONSHORE SPECIALTY INSURANCE COMPANY - POLICY DES: POLITICAL RISK QUOTA SHARE REINSURANCE CONTRACT - U/W REF: TBA - PERIOD FROM: 01 OCT 2015 - PERIOD TO: 31 DEC 2015 - PRO RATA",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447620883",
            Name:
              "REVIEWED BLC; CONNERS, KERRY; RE-USCLAIMS; FW: IRONSHORE CREDIT & POLITICAL RISKS - 2016 REINSURANCE RENEWAL",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447636197",
            Name:
              "REVIEWED BLC; CONNERS, KERRY; RE-USCLAIMS; FW: IRONSHORE CREDIT & POLITICAL RISKS - 2016 REINSURANCE RENEWAL",
            Container: "Document",
            Contents: null,
            Metadata: null
          },
          {
            ID: "447650647",
            Name:
              "REVIEWED BLC; LUCAS-CHIN, BARBARA; LUCAS-CHIN, BARBARA; FW: IRONSHORE TRADE CREDIT AND PRI CLAIM REPORT",
            Container: "Document",
            Contents: null,
            Metadata: null
          }
        ],
        Metadata: [
          {
            Name: "folder type",
            Value: "Claims Related",
            ContainerLevel: "Folder"
          },
          {
            Name: "file number",
            Value: "103324",
            ContainerLevel: "File"
          }
        ]
      },
      {
        ID: "492115984",
        Name: "Finance",
        Container: "Folder",
        Contents: [
          {
            ID: "492115985",
            Name:
              "; FW: Ironshore Specialty - Political Risks QS - Our Ref 13289H; Florencio, Jerson; Konikowski, Ellen",
            Container: "Document",
            Contents: null,
            Metadata: null
          }
        ],
        Metadata: [
          {
            Name: "folder type",
            Value: "Finance",
            ContainerLevel: "Folder"
          },
          {
            Name: "file number",
            Value: "103324",
            ContainerLevel: "File"
          }
        ]
      }
    ],
    Metadata: [
      {
        Name: "file number",
        Value: "103324",
        ContainerLevel: "File"
      },
      {
        Name: "location",
        Value: "",
        ContainerLevel: "File"
      },
      {
        Name: "drawer",
        Value: "Markel Global Re Treaty",
        ContainerLevel: "File"
      },
      {
        Name: "file type",
        Value: "Global Reinsurance",
        ContainerLevel: "File"
      },
      {
        Name: "description",
        Value: "Ironshore 2014 Bond-Credit-Mortgage-Surety QS",
        ContainerLevel: "File"
      },
      {
        Name: "Policy_Number",
        Value: "T1196$2014",
        ContainerLevel: "File"
      },
      {
        Name: "Broker",
        Value: "Willis",
        ContainerLevel: "File"
      },
      {
        Name: "Underwriter",
        Value: "Joscelin Burrer",
        ContainerLevel: "File"
      },
      {
        Name: "Deal Status",
        Value: "Bound",
        ContainerLevel: "File"
      },
      {
        Name: "Contract Number",
        Value: "1196",
        ContainerLevel: "File"
      },
      {
        Name: "Counterparty",
        Value: "Ironshore Speciality Insurance Company",
        ContainerLevel: "File"
      },
      {
        Name: "Underwriter 2",
        Value: "",
        ContainerLevel: "File"
      },
      {
        Name: "Underwriter Asst",
        Value: "",
        ContainerLevel: "File"
      },
      {
        Name: "Expiration_Date",
        Value: "12/31/2014 12:00:00 AM",
        ContainerLevel: "File"
      },
      {
        Name: "Effective_Date",
        Value: "1/1/2014 12:00:00 AM",
        ContainerLevel: "File"
      }
    ]
  };

  getDocJson(): Observable<any> {
    return Observable.of(this.docJson);
  }
}
