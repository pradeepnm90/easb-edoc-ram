import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';

import {LicenseManager} from "ag-grid-enterprise/main";
LicenseManager.setLicenseKey("Markel_Global_Reinsurance_System_1Devs20_December_2018__MTU0NTI2NDAwMDAwMA==217b30490998a7238beb8ed057b6a782");
enableProdMode();


platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));
