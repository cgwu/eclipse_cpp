
(function(a) {
    return eval("[" + a.replace(/\\w{2}/g, function(a) {
        return String.fromCharCode(parseInt(a, 16));
    }) + "]");
})("382C312C352C342C362C332C302C322C372C39");

(function(a) {
    for (var i = 0; i < a.length; i++) {
        a[i] = a[i] + 18 + 9;
    }
    return a;
})([ -19, -20, -24, -18, -25, -27, -23, -26, -22, -21 ]);

(function(a) {
    return function(c, b, a) {
        c.length > b && (a = a.concat(arguments.callee(c, b + 1, [ String.fromCharCode(c[b]) ])));
        b == 0 && (a = eval("[" + a.join("") + "]"));
        return a;
    }(a, 0, []);
})([ 52, 44, 56, 44, 51, 44, 50, 44, 55, 44, 54, 44, 57, 44, 53, 44, 48, 44, 49 ]);
