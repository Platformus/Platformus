/// <reference path="platformus.overlays.fileselectorform.ts" />
/// <reference path="platformus.overlays.objectselectorform.ts" />
module Platformus.Ui {
  declare var tinymce: any;

  export function initializeTinyMce(identity: string): void {
    tinymce.init(
      {
        selector: "#" + identity,
        plugins: [
          "advlist anchor autolink charmap code contextmenu fullscreen image insertdatetime link lists media paste preview print searchreplace table visualblocks",
        ],
        menubar: "edit insert view format table tools",
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist | outdent indent | link image",
        statusbar: false,
        convert_urls: false,
        file_browser_callback: showFileSelectorForm
      }
      );
  }

  export function showFileSelectorForm(identity: string, url: string, code: string, wnd: Window) {
    new Platformus.Overlays.FileSelectorForm(
      function (filename: string) {
        (<HTMLInputElement>wnd.document.getElementById(identity)).value = filename;
      }
    ).show();
  }

  export function showObjectSelectorForm(relationClassId: number, identity: string) {
    new Platformus.Overlays.ObjectSelectorForm(
      relationClassId,
      $("#" + identity).val(),
      function (objectIds: string) {
        $("#" + identity).val(objectIds);
        $("#" + identity).trigger("change");
      }
    ).show();
  }
}