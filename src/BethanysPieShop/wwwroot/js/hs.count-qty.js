/**
 * Count quantity wrapper.
 *
 * @author Htmlstream
 * @version 1.0
 *
 */
;(function ($) {
    'use strict';
  
    $.HSCore.components.HSCountQty = {
      /**
       *
       *
       * @var Object _baseConfig
       */
      _baseConfig: {},
  
      /**
       *
       *
       * @var jQuery pageCollection
       */
      pageCollection: $(),
  
      /**
       * Initialization of Count quantity wrapper.
       *
       * @param String selector (optional)
       * @param Object config (optional)
       *
       * @return jQuery pageCollection - collection of initialized items.
       */
  
      init: function (selector, config) {
  
        this.collection = selector && $(selector).length ? $(selector) : $();
        if (!$(selector).length) return;
  
        this.config = config && $.isPlainObject(config) ?
            $.extend({}, this._baseConfig, config) : this._baseConfig;
  
        this.config.itemSelector = selector;
  
        this.initCountQty();
  
        return this.pageCollection;
  
      },
  
      initCountQty: function () {
        //Variables
        var $self = this,
            collection = $self.pageCollection;
  
        //Actions
        this.collection.each(function (i, el) {
          //Variables
          var $this = $(el),
              $plus = $this.find('.js-plus'),
              $minus = $this.find('.js-minus'),
              $delete = $this.find('#js-del'),
              $result = $this.find('.js-result'),
              $toggler = $this.find('.navbar-toggler'),
              resultVal = parseInt($result.val());
  
          $plus.on('click', function (e) {
            e.preventDefault();
  
            resultVal += 1;
  
            $result.val(resultVal);
          });
  
          $minus.on('click', function (e) {
            e.preventDefault();
  
            if (resultVal >= 1) {
              
              resultVal -= 1;
  
              $result.val(resultVal);
              removeFromShopping_Cart(document.getElementById('amou').attributes["data-id"].value, 0);
              // this.attributes["data-id"].value
              if(resultVal == 0){
                //   $delete.cli
                //   $delete.trigger("click")
                //   $delete.click();

                $toggler.trigger('click');
                $toggler.click();
              }
            } else {
              return false;
            }
          });
  
          //Actions
          collection = collection.add($this);
        });
      }
  
    };
  
  })(jQuery);