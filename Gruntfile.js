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
		watch: {
            options: {
                debounceDelay: 1
            },
            templates: {
                files: ['<%= config.files.template_source %>**/*'],
                tasks: ['pug']
            }
        }
	});

	grunt.loadNpmTasks('grunt-contrib-pug');
	grunt.loadNpmTasks('grunt-contrib-watch');
	
    grunt.task.registerTask('dev', ['pug', 'watch']);
    grunt.task.registerTask('prod', ['pug']);
    grunt.task.registerTask('default', ['dev']);
};