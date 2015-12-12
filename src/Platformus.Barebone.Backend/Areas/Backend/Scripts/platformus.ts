module Platformus.String {
  export var empty = "";

  export function isNullOrEmpty(value: string): boolean {
    return value == null || value == empty;
  }
}

module Platformus.Url {
  export class Descriptor {
    public name: string;
    public value: string;
    public takeFromUrl: boolean;
    public skip: boolean;

    public constructor(options: any) {
      this.name = options.name;
      this.value = options.value;
      this.takeFromUrl = options.takeFromUrl;
      this.skip = options.skip;
    }
  }

  export function getParameters(): any {
    var result = {};
    var match,
      search = /([^&=]+)=?([^&]*)/g,
      query = window.location.search.substring(1),
      plus = /\+/g,
      decode = function (s) { return decodeURIComponent(s.replace(plus, " ")); };

    while (match = search.exec(query)) {
      result[decode(match[1])] = decode(match[2]);
    }

    return result;
  }

  export function combine(descriptors: Array<Descriptor>): string {
    var result = String.empty;
    var parameters = Url.getParameters();

    descriptors.forEach(
      function (descriptor: Descriptor) {
        if (!descriptor.skip) {
          var value = descriptor.takeFromUrl ? parameters[descriptor.name] : descriptor.value;

          if (!String.isNullOrEmpty(value)) {
            result += (result.length == 0 ? "?" : "&") + descriptor.name + "=" + value;
          }
        }
      }
      );

    for (var parameter in parameters) {
      if (!descriptors.some(d => d.name == parameter)) {
        var value = parameters[parameter];

        if (!String.isNullOrEmpty(value)) {
          result += (result.length == 0 ? "?" : "&") + parameter + "=" + value;
        }
      }
    }

    return location.pathname + result;
  }
}