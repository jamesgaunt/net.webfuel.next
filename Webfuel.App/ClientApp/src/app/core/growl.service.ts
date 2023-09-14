import { Injectable, TemplateRef } from "@angular/core";

@Injectable()
export class GrowlService {
  constructor() {
  }

  growlSaved() {
    this.growlSuccess("Changes have been saved");
  }

  growlInvalid() {
    this.growlDanger("Please review and fix invalid inputs");
  }

  growlSuccess(message: string) {
  }

  growlDanger(message: string) {
  }

  growlWarning(message: string) {
  }
}

