/// <reference path="../../../scripts/typings/jquery/jquery.d.ts" />
declare module Platformus.Overlays {
    class OverlayBase {
        protected overlay: JQuery;
        constructor();
        show(): boolean;
        hideAndRemove(): boolean;
        protected create(): void;
    }
    var form: FormBase;
    class FormBase extends OverlayBase {
        private formUrl;
        private modal;
        constructor(formUrl: string);
        show(): boolean;
        hideAndRemove(): boolean;
        getOverlay(): JQuery;
        protected create(): void;
        protected position(): void;
        protected load(): void;
        protected bind(): void;
    }
}
