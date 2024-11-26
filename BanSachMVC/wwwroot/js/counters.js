
	// -------------------------------------------------------------
    // Counter One (Chart)
    // -------------------------------------------------------------

    (function () {

        $('.chart_one').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#4285f4',
            trackColor: '#f7f7f7',
            scaleColor: false,
            lineWidth: 8,
            size: 150,
            scaleLength: 1,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());



	// -------------------------------------------------------------
    // Counter Six (Chart)
    // -------------------------------------------------------------

    (function () {

        $('.chart_two_1st').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#4285f4',
            trackColor: '#fff',
            scaleColor: '#fff',
            lineWidth: 30,
            size: 148,
            scaleLength: 1,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());



    (function () {

        $('.chart_two_2nd').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#7197b3',
            trackColor: '#fff',
            scaleColor: '#fff',
            lineWidth: 30,
            size: 148,
            scaleLength: 1,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());


    (function () {

        $('.chart_two_3rd').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#73ca8d',
            trackColor: '#fff',
            scaleColor: '#fff',
            lineWidth: 30,
            size: 148,
            scaleLength: 1,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());


    (function () {

        $('.chart_two_4th').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#ea6a38',
            trackColor: '#fff',
            scaleColor: '#fff',
            lineWidth: 30,
            size: 148,
            scaleLength: 1,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());
	
	// -------------------------------------------------------------
    // Counter Nine (Chart)
    // -------------------------------------------------------------

    (function () {

        $('.chart_three_1st').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#4285f4',
            trackColor: '#f7f7f7',
            scaleColor: '#fff',
            lineWidth: 10,
            size: 170,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());



    (function () {

        $('.chart_three_2nd').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#7197b3',
            trackColor: '#f7f7f7',
            scaleColor: '#fff',
            lineWidth: 10,
            size: 170,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());


    (function () {

        $('.chart_three_3rd').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#73ca8d',
            trackColor: '#f7f7f7',
            scaleColor: '#fff',
            lineWidth: 10,
            size: 170,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());


    (function () {

        $('.chart_three_4th').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#ea6a38',
            trackColor: '#f7f7f7',
            scaleColor: '#fff',
            lineWidth: 10,
            size: 170,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());
	
	//-------------------------------------------------------
    // counter Four
    //-------------------------------------------------------
    (function () {
            $('.counterFour').bind('inview', function(event, visible, visiblePartX, visiblePartY) {
                if (visible) {
                    $(this).find('.timer').each(function () {
                        var $this = $(this);
                        $({ Counter: 0 }).animate({ Counter: $this.text() }, {
                            duration: 2000,
                            easing: 'swing',
                            step: function () {
                                $this.text(Math.ceil(this.Counter));
                            }
                        });
                    });
                    $(this).unbind('inview');
                }
            });

    }());

	// -------------------------------------------------------------
    // Counter (Chart)
    // -------------------------------------------------------------

    (function () {

        $('.chart_five_1st').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#fff',
            trackColor: '#256ade',
            lineWidth: 12,
            size: 175,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());


    (function () {

        $('.chart_five_2nd').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#fff',
            trackColor: '#5783a5',
            lineWidth: 10,
            size: 175,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());

    (function () {

        $('.chart_five_3rd').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#fff',
            trackColor: '#61bf7d',
            lineWidth: 10,
            size: 175,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());

    (function () {

        $('.chart_five_4th').easyPieChart({
            //your configuration goes here
            easing: 'easeOut',
            delay: 3000,
            barColor: '#fff',
            trackColor: '#ce5729',
            scaleColor: '#73c8b7',
            lineWidth: 10,
            size: 175,
            scaleLength: 0,
            lineCap:'square',
            animate: 5000,
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });

    }());

