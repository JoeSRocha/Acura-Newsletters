module.exports = function(grunt) {
	extend = require('util')._extend; // this makes the extend function available to Jade
	Rhythm = require('./scripts/Rhythm'); // adds our own utility functions, visible to Jade
	
	grunt.initConfig({
		config: {
			paths: {
				source: 'source',
				deploy: 'compiled',
			},
			files: {
				template_source: '<%= config.paths.source %>/',
				template_deploy: '<%= config.paths.deploy %>/'
			}
		},		
		pug: {
			compile: {
				options: {
					pretty: true,
					globals: [ "extend", "Rhythm" ],
				},
				files: [
					{
						expand: true, 
						cwd: "<%= config.files.template_source %>", 
						src: "*.pug", 
						dest: "<%= config.files.template_deploy %>", 
						ext: '.html'
					},
				]
			}
		},
		copy: {
			main: {
				files: [
					{expand: true, cwd: 'compiled/', src: ['**'], dest: 'review.web/Content/Review/'},
				]
			}
		},
		watch: {
            options: {
                debounceDelay: 1
            },
            templates: {
                files: ['<%= config.files.template_source %>**/*'],
                tasks: ['pug', 'copy']
            }
        }
	});

	grunt.loadNpmTasks('grunt-contrib-copy');
	grunt.loadNpmTasks('grunt-contrib-pug');
	grunt.loadNpmTasks('grunt-contrib-watch');
	
    grunt.task.registerTask('dev', ['pug', 'copy', 'watch']);
    grunt.task.registerTask('prod', ['pug', 'copy']);
    grunt.task.registerTask('default', ['dev']);
};