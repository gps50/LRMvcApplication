var start = function (uris) {

    univerModel.uris = uris;

    binding.cources.search();
    binding.cources.create();
    binding.cources.update();

    univerController.showCourcesAsync(1);

};

var binding = {
    cources: {
        search: function () {
            $('#search-cource-form').submit(function () {
                var search = $(this).find('input[name=search]').val();
                univerController.showCourcesAsync(undefined, search);
                return false;
            });
        },
        create: function () {
            var createCurceDialog = $('#create-cource-dialog');
            createCurceDialog.dialog({
                autoOpen: false,
                modal: true
            });
            createCurceDialog.submit(function () {
                createCurceDialog.dialog('close');
                var title = $(this).find('input[name=title]').val();
                var description = $(this).find('input[name=description]').val();
                univerController.createCourceAsync(
                    title,
                    description);
                return false;
            });

            $('#create-cource-button').click(function () {
                //createCurceDialog.find('input[name=title]').val('');
                createCurceDialog.dialog('open');
                return false;
            });
        },
        update: function () {
            var updateCurceDialog = $('#update-cource-dialog').dialog({
                autoOpen: false,
                modal: true
            });
            updateCurceDialog.submit(function () {
                updateCurceDialog.dialog('close');
                var id = $(this).find('input[name=id]').val();
                var title = $(this).find('input[name=title]').val();
                var description = $(this).find('input[name=description]').val();
                univerController.updateCourceAsync(
                    id,
                    title,
                    description);
                return false;
            });
        },
        list: function () {
            $('#cources-table a.cource-edit').click(function () {
                var id = $(this).attr('data-item-id');
                var description = $(this).parent().prev().text();
                var title = $(this).parent().prev().prev().text();
                var dialog = $('#update-cource-dialog');
                dialog.find('input[name=id]').val(id);
                dialog.find('input[name=title]').val(title);
                dialog.find('input[name=description]').val(description);
                dialog.dialog('open');
                return false;
            });

            $('#cources-table a.cource-delete').click(function () {
                var id = $(this).attr('data-item-id');
                univerController.deleteCourceAsync(id);
                return false;
            });

            $('.cource-pages-number a').click(function () {
                var pageNumber = $(this).text();
                univerController.showCourcesAsync(pageNumber);
                return false;
            });
        }
    }
};

var univerController = {
    showCourcesAsync: function (pageNumber, search) {
        univerView.showLoading();
        var promise = univerModel.getCourcesAsync(pageNumber, search);
        promise.always(function () {
            univerView.hideLoading();
        });
        promise.done(function (response) {
            univerView.showCources(response);
        });
        promise.fail(function (jqXHR, textStatus, errorThrown) {
            var message = 'Ошибка загрузки списка курсов';
            if (jqXHR.status == 400) {
                message = errorThrown;
            }
            univerView.showError(message);
        });
        return promise;
    },
    createCourceAsync: function (title, description) {
        univerView.showLoading();
        var promise = univerModel.createCourceAsync(title, description);
        promise.always(function () {
            univerView.hideLoading();
        });
        promise.done(function (response) {
            univerView.showCources(response);
        });
        promise.fail(function (jqXHR, textStatus, errorThrown) {
            var message = 'Ошибка создания курса';
            if (jqXHR.status == 400) {
                message = errorThrown;
            }
            univerView.showError(message);
        });
        return promise;
    },
    updateCourceAsync: function (id, title, description) {
        univerView.showLoading();
        var promise = univerModel.updateCourceAsync(id, title, description);
        promise.always(function () {
            univerView.hideLoading();
        });
        promise.done(function (response) {
            univerView.showCources(response);
        });
        promise.fail(function (jqXHR, textStatus, errorThrown) {
            var message = 'Ошибка изменения курса';
            if (jqXHR.status == 400) {
                message = errorThrown;
            }
            else if (jqXHR.status == 404) {
                message = 'Курс не найден';
            }
            univerView.showError(message);
        });
        return promise;
    },
    deleteCourceAsync: function (id) {
        univerView.showLoading();
        var promise = univerModel.deleteCourceAsync(id);
        promise.done(function (response) {
            univerView.showCources(response);
        });
        promise.always(function () {
            univerView.hideLoading();
        });
        promise.fail(function (jqXHR, textStatus, errorThrown) {
            var message = 'Ошибка удаления курса';
            if (jqXHR.status == 400) {
                message = errorThrown;
            }
            else if (jqXHR.status == 404) {
                message = 'Курс не найден';
            }
            univerView.showError(message);
        });
        return promise;
    }

};

var univerModel = {
    uris: null,
    pageNumber: null,
    search: null,
    getCourcesAsync: function (pageNumber, search) {
        var pageNumberValue = (pageNumber === undefined) ? this.pageNumber : pageNumber;
        var searchValue = (search === undefined) ? this.search : search;
        var _this = this;
        var promise = $.ajax({
            url: this.uris.cources.get,
            type: 'GET',
            dataType: 'text',
            cache: false,
            data: {
                pageNumber: pageNumberValue,
                search: searchValue
            }
        });
        promise.done(function (response) {
            _this.pageNumber = pageNumberValue;
            _this.search = searchValue;
        })
        return promise;
    },
    createCourceAsync: function (title, description) {
        var _this = this;
        var promise = $.ajax({
            url: this.uris.cources.create,
            type: 'POST',
            cache: false,
            data: { title: title, description: description }
        }).then(function (response) {
            return _this.getCourcesAsync(
                _this.pageNumber);
        });
        return promise;
    },
    updateCourceAsync: function (id, title, description) {
        var _this = this;
        var promise = $.ajax({
            url: this.uris.cources.update,
            type: 'PUT',
            cache: false,
            data: { id: id, title: title, description: description }
        }).then(function (response) {
            return _this.getCourcesAsync(
                _this.pageNumber);
        });
        return promise;
    },
    deleteCourceAsync: function (id) {
        var _this = this;
        var promise = $.ajax({
            url: this.uris.cources.del,
            type: 'DELETE',
            cache: false,
            data: { id: id }
        }).then(function (response) {
            return _this.getCourcesAsync(
                _this.pageNumber);
        });
        return promise;
    }
};

var univerView = {

    showCources: function (xml) {
        $('#univer-info').html(xml);        
        binding.cources.list();
    },

    showError: function (message) {
        alert(message);
    },

    showLoading: function (message) {
        $('#blocker').show();
    },

    hideLoading: function (message) {
        $('#blocker').hide();
    }

};

