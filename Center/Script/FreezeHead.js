$.fn.FreezeHead = function(options) {
    var defaults = {
        speed: 3,
        limit: 20
    };
    var options = $.extend({}, defaults, options);

    return this.each(function() {
        var rowcount = $(this).find('tbody tr').length;
        var index = 0;
        $('html').bind('mousewheel', function(event, delta) {
            if (delta < 0) {

                if (index < rowcount - options['limit']) {
                    $(this).find('tbody tr').slice(index, index + options['speed']).css('display', 'none');
                    index = index + options['speed'];
                }
            }
            else {

                if (index >= options['speed']) {
                    $(this).find('tbody tr').slice(index - options['speed'], index).css('display', '');
                    index = index - options['speed'];
                }
            }
        });

    });

};