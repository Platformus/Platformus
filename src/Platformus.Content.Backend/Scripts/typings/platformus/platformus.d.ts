declare module Platformus.String {
    var empty: string;
    function isNullOrEmpty(value: string): boolean;
}
declare module Platformus.Url {
    class Descriptor {
        name: string;
        value: string;
        takeFromUrl: boolean;
        skip: boolean;
        constructor(options: any);
    }
    function getParameters(): any;
    function combine(descriptors: Array<Descriptor>): string;
}
