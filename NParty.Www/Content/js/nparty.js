function printRelatedPostsByLabels(currentArticleId, labels, listId, domain) {
    if (!domain)
        domain = "Nintendo";

    if (labels) {
        var doneCount = 0;
        var articles = [];
        for(var i = 0; i < labels.length; i++){
            $.get('/' + domain + '/Artigos/GetRelatedPosts/?label=' + labels[i], function (data) {
                if (data) {
                    $.each(data, function (j) {
                        articles.push(data[j]);
                    });

                    doneCount++;

                    if (doneCount == labels.length) {
                        articles = removeDuplicatedPostsFromRelated(currentArticleId, articles);
                        articles = shuffleArray(articles);
                        printRelatedArticles(articles, listId);
                    }
                }
            });
        }
    }
}

function removeDuplicatedPostsFromRelated(currentArticleId, relatedPosts) {
    var uniquePosts = [];
    $.each(relatedPosts, function (i, relatedEl) {
        var found = false;

        if (currentArticleId == relatedEl.Id)
        {
            found = true;
        }
        else
            $.each(uniquePosts, function (i, uniqueEl) {
                if (uniqueEl.Id === relatedEl.Id)
                    found = true;
            });

        if(found == false)
            uniquePosts.push(relatedEl);
    });

    return uniquePosts;
}

function shuffleArray(array) {
    for (var j, x, i = array.length; i; j = Math.floor(Math.random() * i), x = array[--i], array[i] = array[j], array[j] = x);
    return array;
}

function printRelatedArticles(articles, listId) {
    $listEl = $('#' + listId);

    $listEl.html('');

    if (!articles || articles.length == 0) {
        $listEl.parent().hide(); 
        return;
    }

    $.each(articles, function (i, article) {
        if (i > 2)
            return;

        var appendable = '<div class="col-md-4 hilight-md-article-item" style="margin-bottom: 20px;">' +
                         '<a href="/' + article.NPartyArticleLink + '">' +
                         '<div><figure style="background-image: url(' + article.CoverImage + ')"></figure></div>' +
                         '<div><h5>' + article.Title + '</h5></div>'
                         '</a></div>';
        $listEl.append(appendable);
    });
}

/* ARTICLE ADJUSTMENT */
function adjustArticleImages() {
    if ($(".content img").length > 0) {
        $(".content img").each(
			function (i, obj) {
			    var $this = $(this);
			    if (i == 0) {
			        $this.css("display", "none");
			        if ($this.parent().parent().next().is("br")) {
			            $this.parent().parent().next().css("display", "none");
			        }
			    } else {
			        if ($this.attr("width") >= 515 || $this.attr("height") == 640) {
			            $this.attr("width", "920");
			            var height = (920 * $this.attr("height"))
								/ $this.attr("width");
			            $this.attr("height", height);
			            $this.css("margin-left", "auto");
			            $this.css("margin-right", "auto");
			        } else if ($this.attr("width") == 250 ||
							   $this.attr("width") == 320 ||
							   $this.attr("height") == 320) {
			            $this.css("float", "left");
			            $this.css("width", "50%");
			            $this.parent().css("margin-left", "0px");
			            $this.parent().css("margin-right", "0px");
			            $this.css("margin-bottom", "20px");
			        }
			        else if ($this.attr("width") == 162 ||
							 $this.attr("width") == 200 ||
							 $this.attr("height") == 200) {
			            $this.css("float", "left");
			            $this.css("width", "33%");
			            $this.parent().css("margin-left", "0px");
			            $this.parent().css("margin-right", "0px");
			            $this.css("margin-bottom", "20px");
			        }

			        if ($this.parent().css('float') == 'right') {
			            $this.parent().css("margin-bottom", "0.5em");
			            $this.parent().css("margin-top", "0.5em");
			            $this.parent().css("margin-left", "0");
			            $this.parent().css("margin-right", "0");
			        }

			        $(this).addClass("img-responsive");
			    }
			});
    } else {
        $('.header').css("height", 0);
        $('#articleSocialLinks').hide();
    }
}

function adjustTables(){
    $(".content table").each(function (i, obj) {
        var $this = $(this);
        $this.addClass("table");
        $this.wrap("<div class='table-responsive'/>");
    });
}

function adjustArticleIframes() {
    $("#article-content iframe").each(function (i, obj) {
        var $this = $(this);
        $this.attr("src", $(this).attr("src") + ($this.attr("src").indexOf("?") > -1 ? '&' : '?') + 'html5=1');
        $this.addClass('embed-responsive-item');
		
        $this.wrap("<div class='embed-responsive embed-responsive-16by9'/>");
        $this.wrap("<div/>");
    });
}
	
function adjustArticleSubtitles(){
    $(".content span").each(
		function (i, obj) {
		    var $this = $(this);
		    if ($this.css('font-size') == '24px') {
		        if ($this.hasClass("pagina-titulo") == false) {
		            $this.replaceWith('<div class="pagina-titulo">' + $this.text() + '</div>');
		        }
		    }
		}			
	);
}

function goToSectionFromId(sectionId) {
    $('html, body').animate({
        scrollTop: $('#' + sectionId).offset().top
    }, 1000);

    return false;
}

function goToSectionFromEl(el) {
    $('html, body').animate({
        scrollTop: el.offset().top
    }, 1000);

    return false;
}

$.fn.scrollTo = function (target, options, callback) {
    if (typeof options == 'function' && arguments.length == 2) { callback = options; options = target; }
    var settings = $.extend({
        scrollTarget: target,
        offsetTop: 50,
        duration: 500,
        easing: 'swing'
    }, options);
    return this.each(function () {
        var scrollPane = $(this);
        var scrollTarget = (typeof settings.scrollTarget == "number") ? settings.scrollTarget : $(settings.scrollTarget);
        var scrollY = (typeof scrollTarget == "number") ? scrollTarget : scrollTarget.offset().top + scrollPane.scrollTop() - parseInt(settings.offsetTop);
        scrollPane.animate({ scrollTop: scrollY }, parseInt(settings.duration), settings.easing, function () {
            if (typeof callback == 'function') { callback.call(this); }
        });
    });
}